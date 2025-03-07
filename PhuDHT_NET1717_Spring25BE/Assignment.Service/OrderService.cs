using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Data;
using Assignment.Data.Common;
using Assignment.Data.Models;
using Assignment.Service.Base;
using Assignment.Service.Dto;
using AutoMapper;

namespace Assignment.Service
{
    public interface IOrderService
    {
        IQueryable<Order> GetAllOdata();
        Task<BaseService> GetAll();
        Task<BaseService> GetById(Guid orderId);
        Task<BaseService> Save(OrderCreateDto order);
        Task<BaseService> DeleteById(Guid orderId);
        Task<BaseService> GetAllAsync();
    }
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<BaseService> DeleteById(Guid orderId)
        {
            try
            {
                var result = false;
                var orderResult = this.GetById(orderId);

                if (orderResult != null && orderResult.Result.Status == Const.SUCCESS_READ_CODE)
                {
                    result = await _unitOfWork.OrderRepository.RemoveAsync((Order)orderResult.Result.Data);

                    if (result)
                    {
                        return new BaseService(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                    }
                    else
                    {
                        return new BaseService(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, orderResult.Result.Data);
                    }
                }
                else
                {
                    return new BaseService(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BaseService(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<BaseService> GetAll()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();

            if (orders == null)
            {
                return new BaseService(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BaseService(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
            }
        }

        public async Task<BaseService> GetAllAsync()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllOrdersAsync();
                Console.WriteLine($"Number of users retrieved: {orders?.Count}");
                if (orders != null && orders.Count > 0)
                {
                    return new BaseService(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
                }
                else
                {
                    return new BaseService(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, null);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in GetAllOrdersAsync: {ex.Message}");
                return new BaseService(Const.ERROR_EXCEPTION, ex.Message, null);
            }
        }

        public IQueryable<Order> GetAllOdata()
        {
            return _unitOfWork.OrderRepository.GetAllOdata();
        }

        public async Task<BaseService> GetById(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdOrderAsync(orderId);
            if (order == null)
            {
                return new BaseService(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BaseService(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
            }
        }

        public async Task<BaseService> Save(OrderCreateDto orderDto)
        {
            try
            {
                int result = -1;
                var newOrder = _mapper.Map<Order>(orderDto);
                newOrder.Id = Guid.NewGuid();
                newOrder.CreatedAt = DateTime.UtcNow;
                newOrder.UpdatedAt = DateTime.UtcNow;
                result = await _unitOfWork.OrderRepository.CreateAsync(newOrder);

                if (result > 0)
                {
                    return new BaseService(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, orderDto);
                }
                else
                {
                    return new BaseService(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BaseService(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

    }
}

using Assignment.Data.Models;
using Assignment.Data.Repository;

namespace Assignment.Data
{
    public class UnitOfWork
    {
        private TutoringKidDbContext _unitOfWorkContext;
        private OrderRepository _orderRepository;
        private UserRepository _userRepository;

        public UnitOfWork()
        {
            _unitOfWorkContext ??= new TutoringKidDbContext();
        }

        public OrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new Repository.OrderRepository(_unitOfWorkContext);
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new Repository.UserRepository(_unitOfWorkContext);
            }
        }
    }
}

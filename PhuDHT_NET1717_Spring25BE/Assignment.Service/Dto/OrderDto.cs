using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Assignment.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment.Service.Dto
{
    public class OrderCreateDto
    {
        public float TotalAmount { get; set; }

        public string Status { get; set; }

        public string PaymentMethod { get; set; }

        public Guid CreatedBy { get; set; }
    }



    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderCreateDto>().ReverseMap();
        }
    }
}

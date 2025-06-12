using AutoMapper;
using OrderManagementSystem.DTOS;
using OrderManagementSystem.Models;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<Order, OrderResponseDTO>();
    }
}
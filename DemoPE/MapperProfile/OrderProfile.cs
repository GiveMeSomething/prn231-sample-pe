using System;
using AutoMapper;
using DemoPE.DTOs;
using DemoPE.Models;

namespace DemoPE.MapperProfile
{
	public class OrderProfile: Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, GetOrderDTO>()
				.ForMember(dest => dest.CustomerName,
					opt => opt.MapFrom(src => src.Customer.CompanyName))
				.ForMember(dest => dest.EmployeeName,
					opt => opt.MapFrom(
						src => src.Employee.FirstName + " " + src.Employee.LastName))
				.ForMember(dest => dest.EmployeeDepartmentId,
					opt => opt.MapFrom(src => src.Employee.Department.DepartmentId))
				.ForMember(dest => dest.EmployeeDepartmentName,
					opt => opt.MapFrom(src => src.Employee.Department.DepartmentName));
		}
	}
}


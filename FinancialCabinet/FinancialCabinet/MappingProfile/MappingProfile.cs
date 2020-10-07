﻿using AutoMapper;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ForMember(dest => dest.DepositList, opt => opt.MapFrom(src => src.LikeDepositList == null ? new List<Deposit> { } : src.LikeDepositList.Select(e => e.Deposit).ToList()));
            CreateMap<UserModel, User>().ForMember(dest => dest.LikeDepositList, opt => opt.MapFrom(src => src.DepositList == null ? new List<LikeDeposit> { } : src.DepositList.Select(e => new LikeDeposit
            {
                DepositID = e.ID
            })));

            CreateMap<Deposit, DepositModel>().ForMember(dest => dest.UserList, opt => opt.MapFrom(src => src.LikeDepositList == null ? new List<User> { } : src.LikeDepositList.Select(e => e.User).ToList()));
            CreateMap<DepositModel, Deposit>().ForMember(dest => dest.LikeDepositList, opt => opt.MapFrom(src => src.UserList == null ? new List<LikeDeposit> { } : src.UserList.Select(e => new LikeDeposit
            {
                DepositID = src.ID
            })));

            CreateMap<Credit, CreditModel>().ForMember(dest => dest.Bank, opt => opt.MapFrom(src => src.Bank));
            CreateMap<CreditModel, Credit>().ForMember(dest => dest.Bank, opt => opt.MapFrom(src => src.Bank));

            CreateMap<Bank, BankModel>();
        }
    }
}

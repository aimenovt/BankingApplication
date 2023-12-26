using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Dto;
using TransactionData.Models;

namespace TransactionData.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Transfer
            CreateMap<AddTransferLogDto, TransferLog>();
            CreateMap<AddTransferLogDto, TransferLog>().ReverseMap();

            CreateMap<GetTransferLogDto, TransferLog>();
            CreateMap<GetTransferLogDto, TransferLog>().ReverseMap();
        }
    }
}

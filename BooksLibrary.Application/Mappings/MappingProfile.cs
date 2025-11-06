using AutoMapper;
using BooksLibrary.Application.DTOs;
using BooksLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Application.Mappings
{
    public class MappingProfile : Profile
    { 
        public MappingProfile() {
            CreateMap<Book, BookDTO>();
            CreateMap<CreateBookDTO, Book>();
            CreateMap<Book, CreateBookDTO>();
            CreateMap<Review, ReviewDTO>();
        }
    }
}

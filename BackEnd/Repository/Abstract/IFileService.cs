﻿using Microsoft.AspNetCore.Http;
using System;

namespace BackEnd.Repository.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);

        public string GetFilePath(string imageStr);
    }
}

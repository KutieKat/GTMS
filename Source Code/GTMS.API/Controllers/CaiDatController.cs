﻿using AutoMapper;
using GTMS.API.Data.CaiDatData;
using GTMS.API.Dtos.CaiDatDto;
using GTMS.API.Dtos.ResponseDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CaiDatController : ControllerBase
    {
        private readonly ICaiDatRepository _repo;
        private readonly IMapper _mapper;
        private readonly string _entityName;

        public CaiDatController(ICaiDatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _entityName = "cài đặt";
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repo.GetAll();
                var resultToReturn = _mapper.Map<CaiDatForViewDto>(result);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = resultToReturn
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, CaiDatForUpdateDto caiDat)
        {
            try
            {
                var result = await _repo.UpdateById(id, caiDat);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Cập nhật " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Cập nhật " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Restore()
        {
            try
            {
                var result = await _repo.Restore();

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Khôi phục " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Khôi phục " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }
    }
}


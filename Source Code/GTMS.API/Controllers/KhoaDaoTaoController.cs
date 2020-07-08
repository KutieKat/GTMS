using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GTMS.API.Data.KhoaDaoTaoData;
using GTMS.API.Dtos.KhoaDaoTaoDto;
using GTMS.API.Dtos.ResponseDto;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GTMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KhoaDaoTaoController : ControllerBase
    {
        private readonly IKhoaDaoTaoRepository _repo;
        private readonly IMapper _mapper;
        private readonly string _entityName;

        public KhoaDaoTaoController(IKhoaDaoTaoRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _entityName = "khóa đào tạo";
        }

        //[Authorize(Roles = "NQL")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] KhoaDaoTaoParams userParams)
        {
            try
            {
                var result = await _repo.GetAll(userParams);
                var resultToReturn = _mapper.Map<IEnumerable<KhoaDaoTaoForListDto>>(result);

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = resultToReturn,
                        TotalItems = _repo.GetTotalItems(),
                        TotalPages = _repo.GetTotalPages(),
                        PageNumber = userParams.PageNumber,
                        PageSize = userParams.PageSize,
                        StatusStatistics = _repo.GetStatusStatistics(userParams)
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

        //[Authorize(Roles = "NQL")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _repo.GetById(id);
                var resultToReturn = _mapper.Map<KhoaDaoTaoForViewDto>(result);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy thông tin chi tiết của " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = resultToReturn
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy thông tin chi tiết của " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        //[Authorize(Roles = "NQL")]
        [HttpPost]
        public async Task<IActionResult> Create(KhoaDaoTaoForCreateDto khoaDaoTao)
        {
            try
            {
                var validationResult = _repo.ValidateBeforeCreate(khoaDaoTao);

                if (validationResult.IsValid)
                {
                    var result = await _repo.Create(khoaDaoTao);

                    return StatusCode(201, new SuccessResponseDto
                    {
                        Message = "Tạo " + _entityName + " mới thành công!",
                        Result = new SuccessResponseResultWithSingleDataDto
                        {
                            Data = result
                        }
                    });
                }
                else
                {
                    return StatusCode(500, new FailedResponseDto
                    {
                        Message = "Tạo " + _entityName + " mới thất bại!",
                        Result = new FailedResponseResultDto
                        {
                            Errors = validationResult.Errors
                        }
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Tạo " + _entityName + " mới thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultiple(ICollection<KhoaDaoTaoForCreateMultipleDto> danhSachKhoaDaoTao)
        {
            try
            {
                var validationResult = _repo.ValidateBeforeCreateMultiple(danhSachKhoaDaoTao);

                if (validationResult.IsValid)
                {
                    var result = await _repo.CreateMultiple(danhSachKhoaDaoTao);

                    return StatusCode(201, new SuccessResponseDto
                    {
                        Message = "Nhập dữ liệu cho " + _entityName + " thành công!",
                        Result = new SuccessResponseResultWithSingleDataDto
                        {
                            Data = result
                        }
                    });
                }
                else
                {
                    return StatusCode(500, new FailedResponseDto
                    {
                        Message = "Nhập dữ liệu cho " + _entityName + " thất bại!",
                        Result = new FailedResponseResultDto
                        {
                            Errors = validationResult.Errors
                        }
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Nhập dữ liệu cho " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }
        //[Authorize(Roles = "NQL")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, KhoaDaoTaoForUpdateDto khoaDaoTao)
        {
            try
            {
                var validationResult = _repo.ValidateBeforeUpdate(id, khoaDaoTao);

                if (validationResult.IsValid)
                {
                    var result = await _repo.UpdateById(id, khoaDaoTao);

                    return StatusCode(200, new SuccessResponseDto
                    {
                        Message = "Cập nhật " + _entityName + " thành công!",
                        Result = new SuccessResponseResultWithSingleDataDto
                        {
                            Data = result
                        }
                    });
                }
                else
                {
                    return StatusCode(500, new FailedResponseDto
                    {
                        Message = "Cập nhật " + _entityName + " mới thất bại!",
                        Result = new FailedResponseResultDto
                        {
                            Errors = validationResult.Errors
                        }
                    });
                }
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

        [HttpPut("{id}")]
        public async Task<IActionResult> TemporarilyDeleteById(int id)
        {
            try
            {
                var result = await _repo.TemporarilyDeleteById(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Xóa tạm thời " + _entityName + " thành công!",
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
                    Message = "Xóa tạm thời " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> RestoreById(int id)
        {
            try
            {
                var result = await _repo.RestoreById(id);

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
        //[Authorize(Roles = "NQL")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> PermanentlyDeleteById(int id)
        {
            try
            {
                var result = await _repo.PermanentlyDeleteById(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Xóa " + _entityName + " thành công!",
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
                    Message = "Xóa " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }
    }
}

using AutoMapper;
using GTMS.API.Data.ThanhVienHoiDongBaoVeData;
using GTMS.API.Dtos.ThanhVienHDBV;
using GTMS.API.Helpers;
using GTMS.API.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTMS.API.Controllers
{
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class ThanhVienHDBVController : ControllerBase
        {
            private readonly IThanhVienHDBVRepository _repo;
            private readonly IMapper _mapper;

            public ThanhVienHDBVController(IThanhVienHDBVRepository repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

        //    [Authorize(Roles = "NQL")]
            [HttpGet]
            public async Task<IActionResult> GetAll([FromQuery] ThanhVienHDBVParams userParams)
            {
                var result = await _repo.GetAll(userParams);
                var resultToReturn = _mapper.Map<IEnumerable<ThanhVienHDBVForListDto>>(result);
                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

                return Ok(resultToReturn);
            }

        //    [Authorize(Roles = "NQL")]
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var result = await _repo.GetById(id);
                var resultToReturn = _mapper.Map<ThanhVienHDBVForViewDto>(result);

                return Ok(resultToReturn);
            }

        //    [Authorize(Roles = "NQL")]
            [HttpPost]
            public async Task<IActionResult> Create(ThanhVienHDBVForCreateDto thanhVienHDBV)
            {
                var result = await _repo.Create(thanhVienHDBV);

                return StatusCode(201);
            }

        //    [Authorize(Roles = "NQL")]
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateById(int id, ThanhVienHDBVForUpdateDto thanhVienHDBV)
            {
                var result = await _repo.UpdateById(id, thanhVienHDBV);

                return StatusCode(200);
            }

         //   [Authorize(Roles = "NQL")]
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteById(int id)
            {
                var result = await _repo.DeleteById(id);

                return StatusCode(200);
            }
        }
    }


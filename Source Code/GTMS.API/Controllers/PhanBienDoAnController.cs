using AutoMapper;
using GTMS.API.Data.PhanBienDoAnData;
using GTMS.API.Dtos.PhanBienDoAnDto;
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
    public class PhanBienDoAnController : ControllerBase
    {
        private readonly IPhanBienDoAnRepository _repo;
        private readonly IMapper _mapper;

        public PhanBienDoAnController(IPhanBienDoAnRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

 //       [Authorize(Roles = "NQL")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PhanBienDoAnParams userParams)
        {
            var result = await _repo.GetAll(userParams);
            var resultToReturn = _mapper.Map<IEnumerable<PhanBienDoAnForListDto>>(result);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

            return Ok(resultToReturn);
        }

 //       [Authorize(Roles = "NQL")]
        [HttpGet("{maDoAn}, {maGiangVien}")]
        public async Task<IActionResult> GetById(int maDoAn, string maGiangVien)
        {
            var result = await _repo.GetById(maDoAn, maGiangVien);
            var resultToReturn = _mapper.Map<PhanBienDoAnForViewDto>(result);

            return Ok(resultToReturn);
        }

//        [Authorize(Roles = "NQL")]
        [HttpPost]
        public async Task<IActionResult> Create(PhanBienDoAnForCreateDto lop)
        {
            var result = await _repo.Create(lop);

            return StatusCode(201);
        }

 //       [Authorize(Roles = "NQL")]
        [HttpPut("{maDoAn}, {maGiangVien}")]
        public async Task<IActionResult> UpdateById(int maDoAn, string maGiangVien, PhanBienDoAnForUpdateDto phanBienDoAn)
        {
            var result = await _repo.UpdateById(maDoAn, maGiangVien, phanBienDoAn);

            return StatusCode(200);
        }

//        [Authorize(Roles = "NQL")]
        [HttpDelete("{maDoAn}, {maGiangVien}")]
        public async Task<IActionResult> DeleteById(int maDoAn, string maGiangVien)
        {
            var result = await _repo.DeleteById(maDoAn, maGiangVien);

            return StatusCode(200);
        }
    }
}

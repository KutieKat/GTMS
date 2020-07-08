using AutoMapper;
using GTMS.API.Data.HuongDanDoAnData;
using GTMS.API.Dtos.HuongDanDoAnDto;
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
    public class HuongDanDoAnController : ControllerBase
    {
        private readonly IHuongDanDoAnRepository _repo;
        private readonly IMapper _mapper;

        public HuongDanDoAnController(IHuongDanDoAnRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

 //       [Authorize(Roles = "NQL")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] HuongDanDoAnParams userParams)
        {
            var result = await _repo.GetAll(userParams);
            var resultToReturn = _mapper.Map<IEnumerable<HuongDanDoAnForListDto>>(result);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

            return Ok(resultToReturn);
        }

//[Authorize(Roles = "NQL")]
        [HttpGet("{maGiangVien}, {maDoAn}")]
        public async Task<IActionResult> GetById(string maGiangVien, int maDoAn)
        {
            var result = await _repo.GetById(maGiangVien, maDoAn);
            var resultToReturn = _mapper.Map<HuongDanDoAnForViewDto>(result);

            return Ok(resultToReturn);
        }

//        [Authorize(Roles = "NQL")]
        [HttpPost]
        public async Task<IActionResult> Create(HuongDanDoAnForCreateDto huongDanDoAn)
        {
            var result = await _repo.Create(huongDanDoAn);

            return StatusCode(201);
        }

 //       [Authorize(Roles = "NQL")]
        [HttpPut("{maGiangVien}, {maDoAn}")]
        public async Task<IActionResult> UpdateById(string maGiangVien, int maDoAn, HuongDanDoAnForUpdateDto huongDanDoAn)
        {
            var result = await _repo.UpdateById(maGiangVien, maDoAn, huongDanDoAn);

            return StatusCode(200);
        }

 //       [Authorize(Roles = "NQL")]
        [HttpDelete("{maGiangVien}, {maDoAn}")]
        public async Task<IActionResult> DeleteById(string maGiangVien, int maDoAn)
        {
            var result = await _repo.DeleteById(maGiangVien, maDoAn);

            return StatusCode(200);
        }
    }
}

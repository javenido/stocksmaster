using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksMasterAPI.DTOs;
using StocksMasterAPI.Models;
using StocksMasterAPI.Services;

namespace StocksMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksDatumsController : ControllerBase
    {
        private readonly StocksMasterDBContext _context;
        private IStocksMasterRepository _stocksMasterRepository;
        private readonly IMapper _mapper;

        public StocksDatumsController(StocksMasterDBContext context, IStocksMasterRepository stocksMasterRepository, IMapper mapper)
        {
            _context = context;

            // Add some data in StocksData table
            SeedData.EnsurePopulated(_context);

            _stocksMasterRepository = stocksMasterRepository;
            _mapper = mapper;
        }

        // GET: api/StocksDatums
        // or GET: api/StocksDatums?includeId=true
        [HttpGet("/api/stocksdata.{format}"), FormatFilter]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StocksDatum>>> GetStocksData(bool includeId = false)
        {
            var stockData = await _stocksMasterRepository.GetStocksData();
            if (includeId)
                return Ok(stockData);
            var results = _mapper.Map<IEnumerable<StocksDatumDto>>(stockData);
            return Ok(results);
        }

        // GET: api/StocksDatums/5
        // or GET: api/StocksDatum/5?includeId=true
        [HttpGet("{id}")]
        public async Task<ActionResult<StocksDatum>> GetStocksDatum(int id, bool includeId = false)
        {
            var stocksDatum = await _stocksMasterRepository.GetStocksDatum(id);

            if (stocksDatum == null)
            {
                return NotFound();
            }

            if (includeId)
                return Ok(stocksDatum);

            var results = _mapper.Map<StocksDatumDto>(stocksDatum);
            return Ok(results);
        }

        // PUT: api/StocksDatums/5
        // or PUT: api/StocksDatums/5?includeId=true
        [HttpPut("{id}")]
        public async Task<ActionResult<StocksDatum>> PutStocksDatum(int id, StocksDatum stocksDatum, bool includeId = false)
        {
            if (id != stocksDatum.StocksDataId)
            {
                return BadRequest();
            }

            var stockDatumRecord = await _stocksMasterRepository.PutStocksDatum(id, stocksDatum);

            if (includeId)
                return Ok(stockDatumRecord);

            var results = _mapper.Map<StocksDatumDto>(stockDatumRecord);
            return Ok(results);
        }

        // PUT: api/StocksDatums/5/500
        [HttpPut("{id}/{price}")]
        public async Task<ActionResult<StocksDatum>> PutStocksDatum(int id, decimal price)
        {
            var stockDatum = await _stocksMasterRepository.GetStocksDatum(id);
            if (stockDatum == null)
                return NotFound();

            stockDatum.StocksPrice = price;
            var stockDatumRecord = await _stocksMasterRepository.PutStocksDatum(id, stockDatum);
            var result = _mapper.Map<StocksDatumDto>(stockDatumRecord);
            return Ok(result);
        }

        // POST: api/StocksDatums
        // or POST: api?StocksDatums?includeId=true
        [HttpPost]
        public async Task<ActionResult<StocksDatum>> PostStocksDatum(StocksDatum stocksDatum, bool includeId = false)
        {
            var stockDatumRecord = await _stocksMasterRepository.PostStocksDatum(stocksDatum);
            if (includeId)
                return Ok(stockDatumRecord);

            var results = _mapper.Map<StocksDatumDto>(stockDatumRecord);
            return Ok(results);
        }

        // DELETE: api/StocksDatums/5
        // or DELETE: api/StocksDatums/5?includeId=true
        [HttpDelete("{id}")]
        public async Task<ActionResult<StocksDatum>> DeleteStocksDatum(int id, bool includeId = false)
        {
            var stockDatumRecord = await _stocksMasterRepository.DeleteStocksDatum(id);

            if (includeId)
                return Ok(stockDatumRecord);

            var results = _mapper.Map<StocksDatumDto>(stockDatumRecord);
            return Ok(results);
        }

        private Task<bool> StocksDatumExists(int id)
        {
            return _stocksMasterRepository.StocksDatumExists(id);
        }
    }
}
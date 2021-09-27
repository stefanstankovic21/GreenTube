using AutoMapper;
using Entity;
using GreenTube.Models;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenTube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletsService _walletsService;
        private readonly IMapper _mapper;

        public WalletsController(IWalletsService walletsService, IMapper mapper)
        {
            _walletsService = walletsService;
            _mapper = mapper;
        }

        [HttpGet("transactons/{playerId}", Name = nameof(GetTransactions))]
        public async Task<IActionResult> GetTransactions(string playerId)
        {
            try
            {
                List<Transaction> transactions = await _walletsService.GetTransactionsForPlayer(playerId);
                if (transactions.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "This player has no transactions!");
                }

                var transactionsModel = _mapper.Map<List<TransactionModel>>(transactions);

                return Ok(transactionsModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Falure");
            }
        }

        [HttpPost("transaction/{playerId}" , Name = nameof(MakeTransaction))]
        public IActionResult MakeTransaction(string playerId, CreateTransactionModel createTransactionModel)
        {
            try
            {
                var transaction = _mapper.Map<Transaction>(createTransactionModel);
                _walletsService.MakeTransaction(playerId, transaction);
                
                return Accepted(transaction.Id);
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.ParamName);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Falure");
            }
        }

        [HttpGet("transacton/{transactionId}", Name = nameof(GetTransaction))]
        public async Task<IActionResult> GetTransaction(string transactionId)
        {
            try
            {
                Transaction transaction = await _walletsService.GetTransactionById(transactionId);
                if (transaction == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Transaction whit this id does not exist!");
                }

                var transactionModel = _mapper.Map<TransactionModel>(transaction);

                return Ok(transactionModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Falure");
            }
        }
    }
}

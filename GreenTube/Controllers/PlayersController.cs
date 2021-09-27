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
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;
        private readonly IMapper _mapper;

        public PlayersController(IPlayersService playersService, IMapper mapper)
        {
            _playersService = playersService;
            _mapper = mapper;
        }

        [HttpPost("register/", Name = nameof(RegisterPlayer))]
        public async Task<IActionResult> RegisterPlayer(CreatePlayerModel createPlayerModel)
        {
            try
            {
                var player = _mapper.Map<Player>(createPlayerModel);

                await _playersService.RegisterPlayer(player);

                var playerModel = _mapper.Map<PlayerModel>(player);

                return Created($"/api/players/{playerModel.Id}", playerModel);
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

        [HttpGet("balance/{playerId}", Name = nameof(GetPlayerBalance))]
        public async Task<IActionResult> GetPlayerBalance(string playerId)
        {
            try
            {
                Player player = await _playersService.GetPlayerByIdAsync(playerId);
                if (player == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Player with this id does not exist!");
                }

                return Ok(player.Wallet.Balance);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Falure");
            }
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayerById(string playerId)
        {
            try
            {
                Player player = await _playersService.GetPlayerByIdAsync(playerId);
                if (player == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Player with this id does not exist!");
                }

                return Ok(player);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Falure");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                List<Player> players = await _playersService.GetPlayers();

                List<PlayerModel> playersModels = _mapper.Map<List<PlayerModel>>(players);

                return Ok(playersModels);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "DataBase Falure");
            }
        }
    }
}

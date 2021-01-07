using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudgetServer.Controllers
{
    [Route("api/AccountType")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AccountTypeController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAccountTypes()
        {
            try
            {
                var accountTypes = _repository.AccountType.GetAllAccountTypes();
                _logger.LogInfo("Returned all accountTypes from database.");

                var accountTypesResult = _mapper.Map<IEnumerable<AccountTypesDTO>>(accountTypes);

                return Ok(accountTypesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccountTypes action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountTypeById(Guid id)
        {
            try
            {
                var accountType = _repository.AccountType.GetAccountTypeById(id);

                if (accountType == null)
                {
                    _logger.LogError($"AccountType with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned AccountType with id: {id}");

                    var accountTypeResult = _mapper.Map<AccountTypesDTO>(accountType);
                    return Ok(accountTypeResult);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAccountTypeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public IActionResult GetAccountTypeWithDetails(Guid id)
        {
            try
            {
                var accountType = _repository.AccountType.GetAccountTypeWithDetails(id);

                if (accountType == null)
                {
                    _logger.LogError($"AccountType with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned accountType with details for id: {id}");

                    var accountTypeResult = _mapper.Map<AccountTypesDTO>(accountType);
                    return Ok(accountTypeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAccountTypeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

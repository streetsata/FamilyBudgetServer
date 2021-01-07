using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.Models;
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

        [HttpGet("{id}", Name = "AccountTypeById")]
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

        [HttpPost]
        public IActionResult CreateAccountType([FromBody] AccountTypeForCreationDto accountType)
        {
            try
            {
                if (accountType == null)
                {
                    _logger.LogError("AccountType object sent from client is null.");
                    return BadRequest("AccountType object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid AccountType object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var accountTypeEntity = _mapper.Map<AccountType>(accountType);

                _repository.AccountType.CreateAccountType(accountTypeEntity);
                _repository.Save();

                var createdAccountType = _mapper.Map<AccountTypesDTO>(accountTypeEntity);

                return CreatedAtRoute("AccountTypeById", new { id = createdAccountType.AccountTypeID }, createdAccountType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccountType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccountType(Guid id, [FromBody] AccountTypeForUpdateDTO accountType)
        {
            try
            {
                if (accountType == null)
                {
                    _logger.LogError("AccountType object sent from client is null.");
                    return BadRequest("AccountType object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid accountType object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var accountTypeEntity = _repository.AccountType.GetAccountTypeById(id);
                if (accountTypeEntity == null)
                {
                    _logger.LogError($"AccountType with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(accountType, accountTypeEntity);
                _repository.AccountType.UpdateAccountType(accountTypeEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAccountType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccountType(Guid id)
        {
            try
            {
                var accountType = _repository.AccountType.GetAccountTypeById(id);
                if (accountType == null)
                {
                    _logger.LogError($"AccountType with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Account.AccountsByAccountType(id).Any())
                {
                    _logger.LogError($"Cannot delete AccountType with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete AccountType. It has related accounts. Delete those accounts first");
                }

                _repository.AccountType.DeleteAccountType(accountType);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAccountType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

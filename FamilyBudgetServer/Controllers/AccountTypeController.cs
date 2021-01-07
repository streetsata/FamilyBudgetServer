﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Gets the list of all Account Types.
        /// </summary>
        /// <returns>The list of Account Types.</returns>
        /// <response code="200">Return all an Account Types</response>  
        /// <response code="500">Internal server error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<IActionResult> GetAllAccountTypes()
        {
            try
            {
                var accountTypes = await _repository.AccountType.GetAllAccountTypesAsync();
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

        /// <summary>
        /// Gets the Account Type by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An Account Type</returns>
        /// <response code="200">Return an Account Type by id</response>  
        /// <response code="404">AccountType with id, hasn't been found in db.</response>  
        /// <response code="500">Internal server error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet("{id}", Name = "AccountTypeById")]
        public async Task<IActionResult> GetAccountTypeById(Guid id)
        {
            try
            {
                var accountType = await _repository.AccountType.GetAccountTypeByIdAsync(id);

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

        /// <summary>
        /// Get Account Type with details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An Account Type with details</returns>
        /// <response code="200">Return an Account Type details by id</response>  
        /// <response code="404">AccountType with id, hasn't been found in db.</response>  
        /// <response code="500">Internal server error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet("{id}/account")]
        public async Task<IActionResult> GetAccountTypeWithDetails(Guid id)
        {
            try
            {
                var accountType = await _repository.AccountType.GetAccountTypeWithDetailsAsync(id);

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

        /// <summary>
        /// Creates an Account Type.
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>A newly created an Account Type</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        /// <response code="500">Internal server error</response>  
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAccountType([FromBody] AccountTypeForCreationDto accountType)
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
                await _repository.SaveAsync();

                var createdAccountType = _mapper.Map<AccountTypesDTO>(accountTypeEntity);

                return CreatedAtRoute("AccountTypeById", new { id = createdAccountType.AccountTypeID }, createdAccountType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccountType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update an Account Type.
        /// </summary>
        /// <param name="id">Account Types Guid</param>
        /// <param name="accountType">Account Type </param>
        /// <returns></returns>
        /// <response code="204">No Content. That's mean that the entity has been updated</response>
        /// <response code="400">Invalid accountType object sent from client.</response>  
        /// <response code="404">AccountType with id, hasn't been found in db.</response>  
        /// <response code="500">Internal server error</response>  
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountType(Guid id, [FromBody] AccountTypeForUpdateDTO accountType)
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
                var accountTypeEntity = await _repository.AccountType.GetAccountTypeByIdAsync(id);
                if (accountTypeEntity == null)
                {
                    _logger.LogError($"AccountType with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(accountType, accountTypeEntity);
                _repository.AccountType.UpdateAccountType(accountTypeEntity);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAccountType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete an Account Type.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">No Content. That's mean that the entity has been updated</response>
        /// <response code="400">Cannot delete AccountType. It has related accounts. Delete those accounts first</response>  
        /// <response code="404">AccountType planed to delete with id, hasn't been found in db.</response>  
        /// <response code="500">Internal server error</response>  
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountType(Guid id)
        {
            try
            {
                var accountType = await _repository.AccountType.GetAccountTypeByIdAsync(id);
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
                await _repository.SaveAsync();
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

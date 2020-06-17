using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Data.Models;
using AuthProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Web.Controllers
{
    public class ProposalController : Controller
    {
        private readonly IAttendeApiService _api;

        public IAuthorizationService _authorizationService { get; }

        public ProposalController(IAttendeApiService api,IAuthorizationService authorizationService)
        {
            _api = api;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index(int conferenceId)
        {
            ViewBag.Title = $"Speaker - Proposals For Conference {conferenceId}";
            ViewBag.ConferenceId = conferenceId;

            return View(await _api.GetAllProposalsForConference(conferenceId));
        }

        [Authorize(Policy = "IsSpeaker")]
        [Authorize(Policy = "YearsOfExperience")]
        public IActionResult AddProposal(int conferenceId)
        {
            ViewBag.Title = "Speaker - Add Proposal";
            return View(new ProposalModel { ConferenceId = conferenceId });
        }

        [HttpPost]
        [Authorize(Policy = "IsSpeaker")]
        [Authorize(Policy ="YearsOfExperience")]
        public async Task<IActionResult> AddProposal(ProposalModel proposal)
        {
            if (ModelState.IsValid)
                await _api.AddProposal(proposal);
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }

        public async Task<IActionResult> Approve(int proposalId)
        {
            var proposal = await _api.ApproveProposal(proposalId);
            return RedirectToAction("Index", new { conferenceId = proposal.ConferenceId });
        }
        [Authorize(Policy = "IsSpeaker")]
        public async Task<IActionResult> EditProposal(int proposalId)
        {
            var proposal = await _api.GetProposal(proposalId);

            var result = await _authorizationService.AuthorizeAsync(User, proposal, "CanEditProposal");
            if (result.Succeeded)
                return View(proposal);
            else
                return RedirectToAction("AccessDenied", "Account");
        }

        [Authorize(Policy = "IsSpeaker")]
        [HttpPost]
        public async Task<IActionResult> EditProposal(ProposalModel proposal)
        {
            if (ModelState.IsValid)
                await _api.EditProposal(proposal);
            return RedirectToAction("Index", new
            {
                conferenceId = proposal.ConferenceId
            });
        }

    }
}
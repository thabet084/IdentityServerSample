using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Data.Models;
using AuthProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Web.Controllers
{
    public class ProposalController : Controller
    {
        private readonly IAttendeApiService _api;

        public ProposalController(IAttendeApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(int conferenceId)
        {
            ViewBag.Title = $"Speaker - Proposals For Conference {conferenceId}";
            ViewBag.ConferenceId = conferenceId;

            return View(await _api.GetAllProposalsForConference(conferenceId));
        }

        public IActionResult AddProposal(int conferenceId)
        {
            ViewBag.Title = "Speaker - Add Proposal";
            return View(new ProposalModel { ConferenceId = conferenceId });
        }

        [HttpPost]
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
    }
}
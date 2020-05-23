using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JqueryAjaxAspMvcNetCore.Models;
using DAO;
using System.Collections.Generic;
using System.Collections;
using static JqueryAjaxAspMvcNetCore.Helper;

namespace JqueryAjaxAspMvcNetCore.Controllers
{
    public class TransactionController : Controller
    {        
        private readonly TransactionDbContext _context;
        public TransactionController(TransactionDbContext context)
        {
            _context = context;            
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transaction/AddOrEdit
        // GET: Transaction/AddOrEdit/5
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var transactionModel = await _context.Transactions.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        // POST: Transaction/AddOrEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        {

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    transactionModel.Date = DateTime.Now;
                    _context.Add(transactionModel);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(transactionModel);
                        await _context.SaveChangesAsync();
                        transactionModel=null;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(transactionModel.TransactionId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }

        
        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transactionModel);
            await _context.SaveChangesAsync();
            return Json(new {html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
        }

        private bool TransactionModelExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}

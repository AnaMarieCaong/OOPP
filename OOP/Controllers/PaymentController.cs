using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOP.Models;

namespace OOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private static List<Payment> payments = new List<Payment>();

        // GET: api/payments
        [HttpGet]
        public IActionResult GetPayments()
        {
            return Ok(payments);
        }

        // GET: api/payments/{id}
        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = payments.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // POST: api/payments
        [HttpPost]
        public IActionResult CreatePayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest("Payment details are invalid.");
            }

            // For demo purposes, auto-assign Id and set payment date
            payment.Id = payments.Count + 1;
            payment.PaymentDate = DateTime.Now;
            payment.Status = "Pending"; // Set initial status to "Pending"

            payments.Add(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }

        // PUT: api/payments/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] Payment updatedPayment)
        {
            var payment = payments.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            payment.Amount = updatedPayment.Amount;
            payment.PaymentMethod = updatedPayment.PaymentMethod;
            payment.Status = updatedPayment.Status; // Update payment status
            payment.PaymentDate = updatedPayment.PaymentDate;

            return NoContent();
        }

        // DELETE: api/payments/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = payments.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            payments.Remove(payment);
            return NoContent();
        }
    }
}
   

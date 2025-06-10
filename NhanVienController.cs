using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaiTapGTSC_API.Models;
using Microsoft.Data.SqlClient;

namespace BaiTapGTSC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NhanVienController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetAll()
        {
            return await _context.NhanViens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVien>> GetById(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null) return NotFound();
            return nv;
        }

        [HttpPost]
        public async Task<ActionResult<NhanVien>> Create(NhanVien nhanVien)
        {
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();

            int namTinhPhep = DateTime.Now.Year; //co the truyen vao nam (?)
            var param = new SqlParameter("@NamTinhPhep", namTinhPhep);
            _context.Database.ExecuteSqlRaw("EXEC sp_TinhSoNgayPhepConLai @NamTinhPhep", param);

            return CreatedAtAction(nameof(GetById), new { id = nhanVien.Id }, nhanVien);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.Id)
                return BadRequest();

            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.NhanViens.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
                return NotFound();

            var quaTrinhLamViecs = _context.QuaTrinhLamViecs
                .Where(q => q.NhanVienId == id);

            _context.QuaTrinhLamViecs.RemoveRange(quaTrinhLamViecs);

            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

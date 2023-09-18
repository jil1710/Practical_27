using Core_Practical_17.DataContext;
using Core_Practical_17.Models;
using Core_Practical_17.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core_Practical_17.DataAccess
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddStudent(Student student)
        {
            _dbContext.Add(student);
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<Student?> FindStudentById(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task<int> UpdateStudent(int id, Student student)
        {
            var data = await GetStudentById(id);
            if (data == null)
            {
                return 0;
            }
            data.Id = id;
            data.Name = student.Name;
            data.Email = student.Email;
            data.DOB = student.DOB;
            data.Age = student.Age; 
            _dbContext.Update(data);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteStudentById(int id)
        {
            var student = await GetStudentById(id);
            if (student == null)
            {
                return 0;
            }

            _dbContext.Students.Remove(student);
            return await _dbContext.SaveChangesAsync();

        }
    }
}

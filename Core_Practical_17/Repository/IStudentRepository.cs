using Core_Practical_17.Models;

namespace Core_Practical_17.Repository
{
    public interface IStudentRepository
    {
        Task<int> AddStudent(Student student);
        Task<int> DeleteStudentById(int id);
        Task<Student?> FindStudentById(int id);
        Task<Student?> GetStudentById(int id);
        Task<List<Student>> GetStudents();
        Task<int> UpdateStudent(int id, Student student);
    }
}
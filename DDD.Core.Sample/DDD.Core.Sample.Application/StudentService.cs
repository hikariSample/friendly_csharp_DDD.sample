using System.Threading.Tasks;
using DDD.Core.Sample.Application.Interfaces;
using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure.Interfaces;
using DDD.Core.Sample.Repository;

namespace DDD.Core.Sample.Application
{
    public class StudentService :BaseService<Student>, IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        public StudentService(IUnitOfWork unitOfWork,IStudentRepository studentRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }
        public async Task<Student> GetAsync(int id)
        {
            return await _studentRepository.FindAsync(a => a.Id == id);
        }

        public async Task<bool> AddAsync(string name)
        {
            var account = new Student()
            {
                Name = "1"
            };

            await _unitOfWork.AddAsync(account);
            return await _unitOfWork.CommitAsync();
        }
    }
}

using DDD.Core.Sample.Application.DTO;
using DDD.Core.Sample.Application.Interfaces;
using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure.Interfaces;

namespace DDD.Core.Sample.Application
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        public StudentService(IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }
        public async Task<StudentDto?> GetAsync(long id)
        {
            var model = await _studentRepository.FindAsync(a => a.Id == id);
            if (model is null) return null;
            var dto = new StudentDto()
            {
                Id = model.Id,
                Name = model.Name
            };

            return dto;
        }

        public async Task<bool> AddAsync(string name)
        {
            var account = new MStudent()
            {
                Name = name
            };

            bool b = await _unitOfWork.AddAsync(account);
            return b;
        }
    }
}

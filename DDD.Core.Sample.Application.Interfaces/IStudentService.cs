using DDD.Core.Sample.Application.DTO;

namespace DDD.Core.Sample.Application.Interfaces;

public interface IStudentService : IBaseService
{
    /// <summary>
    /// 获得教师信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<StudentDto?> GetAsync(long id);
    /// <summary>
    /// 添加教师
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> AddAsync(string name);
}

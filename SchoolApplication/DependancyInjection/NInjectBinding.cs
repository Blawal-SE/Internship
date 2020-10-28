using IData;
using IRepository;
using Ninject.Modules;
using School.Data;
using School.Repository;
namespace DependancyInjection
{
    public class NInjectBinding : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<ISchoolContextt>().To<SchoolContext>();
                Bind<IStudent>().To<StudentRepository>();
                Bind<IUser>().To<UserRolesRepository>();
                Bind<ICourse>().To<CourseRepository>();
            }
            catch (System.Exception e)
            {

                throw e;
            }

        }
    }
}

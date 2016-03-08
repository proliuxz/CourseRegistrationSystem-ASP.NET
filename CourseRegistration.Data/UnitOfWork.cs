using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace CourseRegistration.Data
{
    public interface IUnitOfWork : IDisposable
    {
      
        IRepository<Instructor> InstructorRepository{ get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<CourseClass> CourseClassRepository { get; }
        IRepository<Registration> RegistrationRepository { get; }
        IRepository<Participant> ParticipantRepository { get; }
        IRepository<Company> CompanyRepository { get; }
        IRepository<CompanyHR> CompanyHRRepository { get; }
        UserManager<ApplicationUser> AppUserManager { get; set; }
        RoleManager<IdentityRole> AppRoleManager { get; }
        UserStore<ApplicationUser> AppUserStore { get; }
        IRepository<Attendance> AttendanceRepository { get; }
        IRepository<Assessment> AssessmentRepository { get; }
        void Save();
    }

    public partial class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        private IRepository<Instructor> _instructorRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Course> _courseRepository;
        private IRepository<CourseClass> _courseClassRepository;
        private IRepository<Registration> _registrationRepository;
        private IRepository<Participant> _participantRepository;
        private IRepository<Company> _companyRepository;
        private IRepository<CompanyHR> _companyHRRepository;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private UserStore<ApplicationUser> _userStore;
        private RoleStore<IdentityRole> _roleStore;

        private IRepository<Attendance> _attendanceRepository;
        private IRepository<Assessment> _assessmentRepository;
        
        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
            
        }

        public static IUnitOfWork Create()
        {
            return new UnitOfWork();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                throw e;
            }
            
        }

        public IRepository<Instructor> InstructorRepository
        {
            get
            {
                if (_instructorRepository == null)
                    _instructorRepository = new BaseRepository<Instructor>(_context);

                return _instructorRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new BaseRepository<Category>(_context);

                return _categoryRepository;
            }
        }

        public IRepository<Course> CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                    _courseRepository = new BaseRepository<Course>(_context);

                return _courseRepository;
            }
        }

        public IRepository<CourseClass> CourseClassRepository
        {
            get
            {
                if (_courseClassRepository == null)
                    _courseClassRepository = new BaseRepository<CourseClass>(_context);

                return _courseClassRepository;
            }
        }

        public IRepository<Registration> RegistrationRepository
        {
            get
            {
                if (_registrationRepository == null)
                    _registrationRepository = new BaseRepository<Registration>(_context);

                return _registrationRepository;
            }
        }

        public IRepository<Participant> ParticipantRepository
        {
            get
            {
                if (_participantRepository == null)
                    _participantRepository = new BaseRepository<Participant>(_context);

                return _participantRepository;
            }
        }

        public IRepository<Company> CompanyRepository
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new BaseRepository<Company>(_context);

                return _companyRepository;
            }
        }

        public IRepository<CompanyHR> CompanyHRRepository
        {
            get
            {
                if (_companyHRRepository == null)
                    _companyHRRepository = new BaseRepository<CompanyHR>(_context);

                return _companyHRRepository;
            }
        }

        public IRepository<Attendance> AttendanceRepository
        {
            get
            {
                if (_attendanceRepository == null)
                    _attendanceRepository = new BaseRepository<Attendance>(_context);

                return _attendanceRepository;
            }
        }

        public IRepository<Assessment> AssessmentRepository
        {
            get
            {
                if (_assessmentRepository == null)
                    _assessmentRepository = new BaseRepository<Assessment>(_context);

                return _assessmentRepository;
            }
        }

        public UserStore<ApplicationUser> AppUserStore
        {
            get
            {
                if (_userStore == null)
                {
                    _userStore = new UserStore<ApplicationUser>(_context);
                    _userStore.AutoSaveChanges = false;
                }

                return _userStore;
            }
        }

        public UserManager<ApplicationUser> AppUserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new UserManager<ApplicationUser>(AppUserStore);
                }

                return _userManager;
            }

            set
            {
                _userManager = value;
            }
        }

        public RoleManager<IdentityRole> AppRoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    _roleStore = new RoleStore<IdentityRole>(_context);
                    _roleManager = new RoleManager<IdentityRole>(_roleStore);
                }
                return _roleManager;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

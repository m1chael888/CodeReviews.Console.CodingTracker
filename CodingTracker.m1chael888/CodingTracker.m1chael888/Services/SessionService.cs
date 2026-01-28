using CodingTracker.m1chael888.Models;
using CodingTracker.m1chael888.Repositories;

namespace CodingTracker.m1chael888.Services
{
    public interface ISessionService
    {
        bool Validate(SessionDto Dto);
        void CallCreate(SessionDto Dto);
        List<SessionDto> CallRead();
        void CallUpdate(SessionDto Dto);
        void CallDelete(long id);
    }

    public class SessionService : ISessionService
    {
        private readonly ISessionRepo _sessionRepo;
        public SessionService(ISessionRepo createSession)
        {
            _sessionRepo = createSession;
        }

        public bool Validate(SessionDto Dto)
        {
            if (DateTime.Parse(Dto.StartTime) > DateTime.Parse(Dto.EndTime))
            {
                return false;
            }
            return true;
        }

        public void CallCreate(SessionDto Dto)
        {
            _sessionRepo.Create(MapModel(Dto));
        }

        public List<SessionDto> CallRead()
        {
            var sessionDtos = new List<SessionDto>();

            foreach (var model in _sessionRepo.Read())
            {
                var dto = new SessionDto
                {
                    Id = model.Id,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Duration = model.Duration
                };
                sessionDtos.Add(dto);
            }
            return sessionDtos;
        }

        public void CallUpdate(SessionDto Dto)
        {
            _sessionRepo.Update(MapModel(Dto));
        }

        public void CallDelete(long id)
        {
            _sessionRepo.Delete(id);
        }

        private SessionModel MapModel(SessionDto dto)
        {
            var model = new SessionModel();

            model.StartTime = dto.StartTime;
            model.EndTime = dto.EndTime;
            model.Duration = dto.Duration;
            if (dto.Id != 0) model.Id = dto.Id;

            return model;
        }
    }
}

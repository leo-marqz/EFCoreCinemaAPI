using AutoMapper;
using EFCoreCinemaAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EFCoreCinemaAPI.Services
{
    public interface IUpdateObservableCollectionService
    {
        void Update<ENT, DTO>(ObservableCollection<ENT> entities, IEnumerable<DTO> dtos)
            where ENT : IId
            where DTO : IId;
    }

    public class UpdateObservableCollectionService : IUpdateObservableCollectionService
    {
        private readonly IMapper mapper;

        public UpdateObservableCollectionService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Update<ENT, DTO>(ObservableCollection<ENT> entities, IEnumerable<DTO> dtos)
            where ENT : IId
            where DTO : IId
        {
            var entitiesDyc = entities.ToDictionary(x => x.Id);
            var dtosDyc = dtos.ToDictionary(x => x.Id);

            var entitiesIds = entitiesDyc.Select(c => c.Key);
            var dtosIds = dtosDyc.Select(c => c.Key);

            var create = dtosIds.Except(entitiesIds);
            var delete = entitiesIds.Except(dtosIds);
            var update = entitiesIds.Intersect(dtosIds);

            foreach(var id in create)
            {
                var entity = mapper.Map<ENT>(dtosDyc[id]);
                entities.Add(entity);
            }

            foreach(var id in delete)
            {
                var entity = entitiesDyc[id];
                entities.Remove(entity);
            }

            foreach (var id in update)
            {
                var dto = dtosDyc[id];
                var entity = entitiesDyc[id];

                entity = mapper.Map(dto, entity);
            }
        }
    }
}

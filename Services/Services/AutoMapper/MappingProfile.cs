using Domain;
using Mapster;
using Services.Models.CowModels;
using Services.Models.FarmModels;
using Services.Models.GalponModels;
using Services.Models.LoteModels;
using Services.Models.ProduccionModels;
using Services.Models.PrecioModels;

namespace Services.Automapper
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // ---- Modulo Ganadero (original) ----
            config.NewConfig<Cow, CowModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Milks, src => src.Milks);

            config.NewConfig<CowModel, Cow>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Milks, src => src.Milks);

            config.NewConfig<Farm, FarmModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(des => des.CowCount, src => src.Cows.Count())
                .Map(dest => dest.Location, src => src.Location);

            config.NewConfig<AddFarmModel, Farm>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Location, src => src.Location);

            // ---- Modulo Avicola ----
            config.NewConfig<Galpon, GalponModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Nombre, src => src.Nombre)
                .Map(dest => dest.Descripcion, src => src.Descripcion)
                .Map(dest => dest.CantidadLotes, src => src.Lotes.Count());

            config.NewConfig<AddGalponModel, Galpon>()
                .Map(dest => dest.Nombre, src => src.Nombre)
                .Map(dest => dest.Descripcion, src => src.Descripcion);

            config.NewConfig<Lote, LoteModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FechaRecepcion, src => src.FechaRecepcion)
                .Map(dest => dest.Raza, src => src.Raza)
                .Map(dest => dest.CantidadInicial, src => src.CantidadInicial)
                .Map(dest => dest.CantidadActual, src => src.GetCantidadActual())
                .Map(dest => dest.MortalidadTotal, src => src.GetMortalidadTotal())
                .Map(dest => dest.TotalHuevosProducidos, src => src.GetTotalHuevos())
                .Map(dest => dest.GalponId, src => src.GalponId)
                .Map(dest => dest.Proposito, src => src.Proposito)
                .Map(dest => dest.Estado, src => src.Estado)
                .Map(dest => dest.GalponNombre, src => src.Galpon != null ? src.Galpon.Nombre : string.Empty);

            config.NewConfig<AddLoteModel, Lote>()
                .Map(dest => dest.Raza, src => src.Raza)
                .Map(dest => dest.Proposito, src => src.Proposito)
                .Map(dest => dest.FechaRecepcion, src => src.FechaRecepcion)
                .Map(dest => dest.CantidadInicial, src => src.CantidadInicial)
                .Map(dest => dest.GalponId, src => src.GalponId);

            config.NewConfig<ProduccionDiaria, ProduccionDiariaModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.LoteId, src => src.LoteId)
                .Map(dest => dest.LoteRaza, src => src.Lote != null ? src.Lote.Raza : string.Empty)
                .Map(dest => dest.Fecha, src => src.Fecha)
                .Map(dest => dest.HuevosAA, src => src.HuevosAA)
                .Map(dest => dest.HuevosA, src => src.HuevosA)
                .Map(dest => dest.HuevosAAA, src => src.HuevosAAA)
                .Map(dest => dest.Rotos, src => src.Rotos)
                .Map(dest => dest.Mortalidad, src => src.Mortalidad)
                .Map(dest => dest.AlimentoConsumidoKg, src => src.AlimentoConsumidoKg)
                .Map(dest => dest.TotalHuevosComerciales, src => src.GetTotalHuevosComerciales());

            config.NewConfig<AddProduccionDiariaModel, ProduccionDiaria>()
                .Map(dest => dest.LoteId, src => src.LoteId)
                .Map(dest => dest.Fecha, src => src.Fecha)
                .Map(dest => dest.HuevosAA, src => src.HuevosAA)
                .Map(dest => dest.HuevosA, src => src.HuevosA)
                .Map(dest => dest.HuevosAAA, src => src.HuevosAAA)
                .Map(dest => dest.Rotos, src => src.Rotos)
                .Map(dest => dest.Mortalidad, src => src.Mortalidad)
                .Map(dest => dest.AlimentoConsumidoKg, src => src.AlimentoConsumidoKg);

            config.NewConfig<InventarioAlimento, Services.Models.InventarioModels.InventarioAlimentoModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.TipoAlimento, src => src.TipoAlimento)
                .Map(dest => dest.CantidadKg, src => src.CantidadKg)
                .Map(dest => dest.LoteId, src => src.LoteId)
                .Map(dest => dest.FechaRegistro, src => src.FechaRegistro)
                .Map(dest => dest.LoteRaza, src => src.Lote != null ? src.Lote.Raza : string.Empty);

            config.NewConfig<Services.Models.InventarioModels.AddInventarioAlimentoModel, InventarioAlimento>()
                .Map(dest => dest.TipoAlimento, src => src.TipoAlimento)
                .Map(dest => dest.CantidadKg, src => src.CantidadKg)
                .Map(dest => dest.LoteId, src => src.LoteId);

            config.NewConfig<RegistroVacunacion, Services.Models.VacunacionModels.RegistroVacunacionModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.LoteId, src => src.LoteId)
                .Map(dest => dest.Fecha, src => src.Fecha)
                .Map(dest => dest.Vacuna, src => src.Vacuna)
                .Map(dest => dest.MetodoAplicacion, src => src.MetodoAplicacion)
                .Map(dest => dest.AvesVacunadas, src => src.AvesVacunadas)
                .Map(dest => dest.Observaciones, src => src.Observaciones)
                .Map(dest => dest.LoteRaza, src => src.Lote != null ? src.Lote.Raza : string.Empty);

            config.NewConfig<Services.Models.VacunacionModels.AddRegistroVacunacionModel, RegistroVacunacion>()
                .Map(dest => dest.LoteId, src => src.LoteId)
                .Map(dest => dest.Fecha, src => src.Fecha)
                .Map(dest => dest.Vacuna, src => src.Vacuna)
                .Map(dest => dest.MetodoAplicacion, src => src.MetodoAplicacion)
                .Map(dest => dest.AvesVacunadas, src => src.AvesVacunadas)
                .Map(dest => dest.Observaciones, src => src.Observaciones);

            // ---- Precios ----
            config.NewConfig<PrecioHuevo, PrecioHuevoModel>();
            config.NewConfig<AddPrecioHuevoModel, PrecioHuevo>();
        }
    }
}

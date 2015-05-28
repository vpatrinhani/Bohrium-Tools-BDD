using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Bohrium.Tools.BDDManagementTool.WebApp.Utils
{

    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Shortcut for the Ignore properties on the AutoMapper.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mapping"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> mapping, Expression<Func<TDestination, object>> property)
        {
            return mapping.ForMember(property, opt => opt.Ignore());
        }

        /// <summary>
        /// Method to help on the Ignore fields on the AutoMapper.
        /// </summary>
        /// <typeparam name="TSource">from Source</typeparam>
        /// <typeparam name="TDestination">to Destination</typeparam>
        /// <param name="mapping">Extension type for AutoMapper</param>
        /// <param name="properties">Properties</param>
        /// <returns>Fluent Auto Mapping</returns>
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> mapping, params Expression<Func<TDestination, object>>[] properties)
        {
            foreach (var property in properties)
                mapping = mapping.ForMember(property, opt => opt.Ignore());

            return mapping;
        }

        /// <summary>
        /// ShortCut for Custom Mapping using MapFrom option.
        /// </summary>
        /// <typeparam name="TSource">from Source</typeparam>
        /// <typeparam name="TDestination">to Destionantion </typeparam>
        /// <typeparam name="TMember">Type of member</typeparam>
        /// <param name="mapping"></param>
        /// <param name="property">Properties</param>
        /// <param name="mapFrom">mapFrom function</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> MapFrom<TSource, TDestination, TMember>
            (this IMappingExpression<TSource, TDestination> mapping, Expression<Func<TDestination, object>> property, Expression<Func<TSource, TMember>> mapFrom)
        {
            return mapping.ForMember(property, opt => opt.MapFrom(mapFrom));
        }

        /// <summary>
        /// ShortCut to map types using AutoMapper.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination To<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
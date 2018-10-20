using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Mapper
{
    /// <summary>
    /// Mapper for objects
    /// </summary>
    public class Mapper : IMapper
    {
        /// <summary>
        /// Mapping operation type.
        /// </summary>
        private MapOpType mapOpType;

        /// <summary>
        /// Creates new instanc eof mapper.
        /// </summary>
        public Mapper()
        {
            //getting mapper operation type from web.config
            var mapOpTypeStr = ConfigurationManager.AppSettings["mapOpType"];

            //initializing map operation type
            this.mapOpType = (MapOpType)Enum.Parse(typeof(MapOpType),mapOpTypeStr);
        }

        /// <summary>
        /// Maps objects.
        /// </summary>
        /// <typeparam name="TSource"> Type of source object. </typeparam>
        /// <typeparam name="TDestination"> Type of destination object. </typeparam>
        /// <param name="sourceObject"> Source object. </param>
        /// <returns> Returns destination object. </returns>
        public TDestination Map<TSource, TDestination>(TSource sourceObject)
        {
            //if map operation type is Expression
            //map using Expressions
            if (this.mapOpType == MapOpType.Expressions)
            {
                return this.MapUsingExpressions<TSource,TDestination>(sourceObject);
            }

            //else use IL emitting
            if (this.mapOpType == MapOpType.IL_Emitting)
            {
                return this.MapUsingIL_Emitting<TSource, TDestination>(sourceObject);
            }

            //else use Reflection
            return MapUsingReflection<TSource, TDestination>(sourceObject);

        }


        /// <summary>
        /// Maps objects using expressions.
        /// </summary>
        /// <typeparam name="TSource"> Type of source object. </typeparam>
        /// <typeparam name="TDestination"> Type of destination object. </typeparam>
        /// <param name="sourceObject"> Source object. </param>
        /// <returns> Returns destination object. </returns>
        private TDestination MapUsingExpressions<TSource, TDestination>(TSource sourceObject) 
        {
            //getting properties of DAL object
            var properties = sourceObject.GetType().GetProperties();
            
            //creating destination object
            var destObj = Activator.CreateInstance<TDestination>();

            //list of assignment binary expressions
            var assignExpressions = new List<BinaryExpression>();

            //source object parameter expression
            var sourceObjParamExp = Expression.Parameter(typeof(TSource));

            //destination object parameter expression
            var destObjParamExpr = Expression.Parameter(typeof(TDestination));

            //constructing assign expressions
            foreach (var propertyInfo in properties)
            {
                //destination object property expression
                var destObjPropExpr = Expression.Property(destObjParamExpr, propertyInfo.Name);

                //source object property expression
                var sourceObjPropExpr = Expression.Property(sourceObjParamExp, propertyInfo.Name);

                //assignment binary expression
                var assignExpr = Expression.Assign(destObjPropExpr, sourceObjPropExpr);

                //adding the assignment expression to the list
                assignExpressions.Add(assignExpr);
            }

            //block expression for the block of expressions
            var blockExpr = Expression.Block(assignExpressions);

            //delegate of dynamic mapper
            var dynamicMapper = Expression.Lambda<
                Action<TSource, TDestination>>(blockExpr, sourceObjParamExp, destObjParamExpr).Compile();

            //calling dynamic mapper
            dynamicMapper(sourceObject, destObj);

            //return the destination object
            return destObj;
        }

        /// <summary>
        /// Maps objects using IL emitting. 
        /// </summary>
        /// <typeparam name="TSource"> Type of source object. </typeparam>
        /// <typeparam name="TDestination"> Type of destination object. </typeparam>
        /// <param name="sourceObject"> Source object. </param>
        /// <returns> Returns destination object. </returns>
        private TDestination MapUsingIL_Emitting<TSource, TDestination>(TSource sourceObject) 
        {
            //dynamic method
            var dynamicMethod = new DynamicMethod("IL_Map", typeof(void),
                new [] { typeof(TSource), typeof(TDestination) },typeof(Mapper).Module);

            //getting IL generator
            var ilGen = dynamicMethod.GetILGenerator();

            //getting properties
            var properties = typeof(TSource).GetProperties();

            //creating destination object
            var destObject = Activator.CreateInstance<TDestination>();

            //constructing dynamic method
            foreach (var propertyInfo in properties)
            {
                //emitting arguments
                ilGen.Emit(OpCodes.Ldarg_1);
                ilGen.Emit(OpCodes.Ldarg_0);

                //getting destination object property
                var destProp = typeof(TDestination).GetProperty(propertyInfo.Name);

                //emitting calls
                ilGen.EmitCall(OpCodes.Callvirt,propertyInfo.GetGetMethod(),null);
                ilGen.EmitCall(OpCodes.Callvirt, destProp.GetSetMethod(), null);
            }

            //emitting return statement
            ilGen.Emit(OpCodes.Ret);

            //invoking dynamic method
            dynamicMethod.Invoke(null, new object []{ sourceObject,destObject });

            //returning destination object
            return destObject;
        }

        /// <summary>
        /// Maps objects using reflection. 
        /// </summary>
        /// <typeparam name="TSource"> Type of source object. </typeparam>
        /// <typeparam name="TDestination"> Type of destination object. </typeparam>
        /// <param name="sourceObject"> Source object. </param>
        /// <returns> Returns destination object. </returns>
        private TDestination MapUsingReflection<TSource, TDestination>(TSource sourceObject)
        {
            //getting propeties
            var properties = typeof(TSource).GetProperties();

            //creating destination object
            var destObj = Activator.CreateInstance<TDestination>();

            //constructing destination object
            foreach (var propertyInfo in properties)
            {
                //getting destination object property
                var destObjProp = typeof(TDestination).GetProperty(propertyInfo.Name);

                //getting source object property value
                var value = propertyInfo.GetValue(sourceObject);

                //setting destination object property
                destObjProp.SetValue(destObj,value);
            }

            //returning destination object
            return destObj;
        }
    }
}

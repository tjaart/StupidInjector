using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StupidInjector
{
    public class StupidContainer
    {
        private readonly Dictionary<Type, Type> _registrations;

        public StupidContainer()
        {
            _registrations = new Dictionary<Type, Type>();
        }

        public void Register<TInterface, TConcrete>() where TConcrete : TInterface // ensure that the concrete actually inherits from the interface
        {
            _registrations[typeof(TInterface)]= typeof(TConcrete);
        }
        
        public void Register<TConcrete>() where TConcrete : class
        {
            _registrations[typeof(TConcrete)]= typeof(TConcrete);
        }


        public object GetInstance(Type t)
        {
            if (!_registrations.ContainsKey(t))
            {
                throw new Exception($"Type {t.Name} is not registered with StupidContainer");
            }

            var typeToInstantiate = _registrations[t];

            var constructorInfo = typeToInstantiate.GetConstructors().First(); // assume only one constructor
            var constructorParameterInstances = GetConstructorParameterInstances(constructorInfo);

            if (constructorParameterInstances.Any())
            {
                // call the constructor to get a new instance of the class with instances of all its dependencies.
                return constructorInfo.Invoke(constructorParameterInstances.ToArray());
            }

            return Activator.CreateInstance(typeToInstantiate);
        }

        private List<object> GetConstructorParameterInstances(ConstructorInfo constructorInfo)
        {
            var constructorParameterInstances = new List<object>();
            var constructorParameterInfos = constructorInfo.GetParameters();

            var constructorParameterTypes = new List<Type>();

            foreach (var parameterInfo in constructorParameterInfos)
            {
                // ensure that all the parameters of the constructor are registered with our container
                if (!_registrations.ContainsKey(parameterInfo.ParameterType))
                {
                    throw new Exception($"Type {parameterInfo.ParameterType.Name} is not registered with StupidContainer");
                }

                constructorParameterTypes.Add(parameterInfo.ParameterType);
            }


            foreach (var constructorParameterType in constructorParameterTypes)
            {
                constructorParameterInstances.Add(GetInstance(constructorParameterType));
            }

            return constructorParameterInstances;
        }

        public TInst GetInstance<TInst>()
        {
            return (TInst) GetInstance(typeof(TInst));
        }
    }
}
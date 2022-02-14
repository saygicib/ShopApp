using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            var lists = invocation.Arguments.Where(t => t.GetType() is IList);

            foreach (var item in lists)
            {
                foreach (var entity in (IList)item)
                {
                    if (item.GetType() == entityType)
                    {
                        ValidationTools.Validate(validator, entity);
                    }
                }
            }
            if (entities is not null)
            {
                foreach (var entity in entities)
                {
                    ValidationTools.Validate(validator, entity);
                }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using reexjungle.xmisc.infrastructure.concretes.operations;

namespace reexjungle.xmisc.infrastructure.concretes.extensions
{
    /// <summary>
    /// Extends features of FluentValidation.
    /// </summary>
    public static class FluentValidationExtensions
    {


        #region [Collection Validators Extensions]

        /// <summary>
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="validators"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCollectionElement"></typeparam>
        /// <returns></returns>
        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement>
            (this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
                IEnumerable<IValidator<TCollectionElement>> validators)
        {
            if (validators == null) throw new ArgumentNullException("validators");

            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.AddRange(validators);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement, TElement1>(
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1)
            where TElement1 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        #region MyRegion
        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
    SetCollectionValidators<T, TCollectionElement, TElement1, TElement2>(
    this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
    IValidator<TElement1> validator1,
    IValidator<TElement2> validator2)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement, TElement1, TElement2, TElement3>(
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement, TElement1, TElement2, TElement3, TElement4>(
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10,
                TElement11>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10,
            IValidator<TElement11> validator11)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
            where TElement11 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            adaptor.Add(validator11);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10,
                TElement11,
                TElement12>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10,
            IValidator<TElement11> validator11,
            IValidator<TElement12> validator12)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
            where TElement11 : TCollectionElement
            where TElement12 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            adaptor.Add(validator11);
            adaptor.Add(validator12);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10,
                TElement11,
                TElement12,
                TElement13>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10,
            IValidator<TElement11> validator11,
            IValidator<TElement12> validator12,
            IValidator<TElement13> validator13)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
            where TElement11 : TCollectionElement
            where TElement12 : TCollectionElement
            where TElement13 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            adaptor.Add(validator11);
            adaptor.Add(validator12);
            adaptor.Add(validator13);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10,
                TElement11,
                TElement12,
                TElement13,
                TElement14>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10,
            IValidator<TElement11> validator11,
            IValidator<TElement12> validator12,
            IValidator<TElement13> validator13,
            IValidator<TElement14> validator14)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
            where TElement11 : TCollectionElement
            where TElement12 : TCollectionElement
            where TElement13 : TCollectionElement
            where TElement14 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            adaptor.Add(validator11);
            adaptor.Add(validator12);
            adaptor.Add(validator13);
            adaptor.Add(validator14);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10,
                TElement11,
                TElement12,
                TElement13,
                TElement14,
                TElement15>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10,
            IValidator<TElement11> validator11,
            IValidator<TElement12> validator12,
            IValidator<TElement13> validator13,
            IValidator<TElement14> validator14,
            IValidator<TElement15> validator15)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
            where TElement11 : TCollectionElement
            where TElement12 : TCollectionElement
            where TElement13 : TCollectionElement
            where TElement14 : TCollectionElement
            where TElement15 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            adaptor.Add(validator11);
            adaptor.Add(validator12);
            adaptor.Add(validator13);
            adaptor.Add(validator14);
            adaptor.Add(validator15);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        }

        public static CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
            SetCollectionValidators<T, TCollectionElement,
                TElement1,
                TElement2,
                TElement3,
                TElement4,
                TElement5,
                TElement6,
                TElement7,
                TElement8,
                TElement9,
                TElement10,
                TElement11,
                TElement12,
                TElement13,
                TElement14,
                TElement15,
                TElement16>
            (
            this IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            IValidator<TElement1> validator1,
            IValidator<TElement2> validator2,
            IValidator<TElement3> validator3,
            IValidator<TElement4> validator4,
            IValidator<TElement5> validator5,
            IValidator<TElement6> validator6,
            IValidator<TElement7> validator7,
            IValidator<TElement8> validator8,
            IValidator<TElement9> validator9,
            IValidator<TElement10> validator10,
            IValidator<TElement11> validator11,
            IValidator<TElement12> validator12,
            IValidator<TElement13> validator13,
            IValidator<TElement14> validator14,
            IValidator<TElement15> validator15,
            IValidator<TElement16> validator16)
            where TElement1 : TCollectionElement
            where TElement2 : TCollectionElement
            where TElement3 : TCollectionElement
            where TElement4 : TCollectionElement
            where TElement5 : TCollectionElement
            where TElement6 : TCollectionElement
            where TElement7 : TCollectionElement
            where TElement8 : TCollectionElement
            where TElement9 : TCollectionElement
            where TElement10 : TCollectionElement
            where TElement11 : TCollectionElement
            where TElement12 : TCollectionElement
            where TElement13 : TCollectionElement
            where TElement14 : TCollectionElement
            where TElement15 : TCollectionElement
            where TElement16 : TCollectionElement
        {
            var adaptor = new ChildCollectionValidationsAdaptor<TCollectionElement>();
            adaptor.Add(validator1);
            adaptor.Add(validator2);
            adaptor.Add(validator3);
            adaptor.Add(validator4);
            adaptor.Add(validator5);
            adaptor.Add(validator6);
            adaptor.Add(validator7);
            adaptor.Add(validator8);
            adaptor.Add(validator9);
            adaptor.Add(validator10);
            adaptor.Add(validator11);
            adaptor.Add(validator12);
            adaptor.Add(validator13);
            adaptor.Add(validator14);
            adaptor.Add(validator15);
            adaptor.Add(validator16);
            ruleBuilder.SetValidator(adaptor);
            return new CollectionValidatorRuleBuilder<T, TCollectionElement>(ruleBuilder, adaptor);
        } 
        #endregion

        #endregion

        internal static ValidationContextExt CloneForChildValidator(this ValidationContext context,
            object instanceToValidate)
        {
            return new ValidationContextExt(instanceToValidate, context.PropertyChain, context.Selector, true);
        }
    }

    internal class ValidationContextExt : ValidationContext
    {
        private readonly bool isChildContext;

        public ValidationContextExt(object instanceToValidate, bool? isChildContext = null)
            : base(instanceToValidate)
        {
            this.isChildContext = isChildContext ?? base.IsChildContext;
        }

        public ValidationContextExt(object instanceToValidate, PropertyChain propertyChain,
            IValidatorSelector validatorSelector, bool? isChildContext = null)
            : base(instanceToValidate, propertyChain, validatorSelector)
        {
            this.isChildContext = isChildContext ?? base.IsChildContext;
        }

        public new bool IsChildContext
        {
            get { return isChildContext; }
        }
    }
}
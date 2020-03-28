using System;
using FluentValidation.Results;

namespace xUnitTestSamples.Features.Core
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool EhValido()
            => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
            => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString()
             => GetType().Name + " [Id=" + Id + "]";
    }
}
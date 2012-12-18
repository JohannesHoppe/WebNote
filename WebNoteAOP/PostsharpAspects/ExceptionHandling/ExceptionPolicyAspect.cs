namespace PostsharpAspects.ExceptionHandling
{
    using System;
    using System.Diagnostics;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;

    /// <summary>
    /// Aspect that, when applied on a method, catches all its exceptions,
    /// assign them a GUID, log them, and replace them by an <see cref="MyException"/>.
    /// </summary>
    [Serializable]
    [ProvideAspectRole(StandardRoles.ExceptionHandling)]
    public class ExceptionPolicyAspect : OnExceptionAspect
    {
        /// <summary>
        /// Method invoked upon failure of the method to which the current
        /// aspect is applied.
        /// </summary>
        /// <param name="args">Information about the method being executed.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            Guid guid = Guid.NewGuid();

            Trace.TraceError("Exception {0} handled by ExceptionPolicyAttribute: {1}", guid, args.Exception.ToString());

            throw new MyException(
                string.Format(
                    "An internal exception has occurred. Use the id {0} " + "for further reference to this issue.", guid));
        }
    }
}
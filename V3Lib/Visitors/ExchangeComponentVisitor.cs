using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class ExchangeComponentVisitor : VisitorBase<Component>
    {
        public ExchangeComponentVisitor(ExchangeComponentParams @params) : base(@params) { }

        public ExchangeComponentParams ExchangeComponentParams => (ExchangeComponentParams) _params;

        public override void Visit(Component element)
        {
            if (element.ContainComponent(ExchangeComponentParams.TargetComponent))
            {
                element.ExchangeComponent(ExchangeComponentParams.TargetComponent, ExchangeComponentParams.ExchangeComponent);
            }
        }
    }

    public class ExchangeComponentVisitorBuilder : VisitorBuilder<ExchangeComponentVisitor, ExchangeComponentParams>
    {
        public ExchangeComponentVisitorBuilder() : base()
        {
            Params = new ExchangeComponentParams();
        }
    }

    public static class ExchangeComponentVisitorBuilderExtensions
    {
        public static IVisitorBuilder<ExchangeComponentVisitor, ExchangeComponentParams> SetExchangeComponent(this IVisitorBuilder<ExchangeComponentVisitor, ExchangeComponentParams> builder, Component target, Component exchange)
        {
            builder.SetExchangeComponent(new ExchangeComponentParams { TargetComponent = target, ExchangeComponent = exchange });
            return builder;
        }

        public static IVisitorBuilder<ExchangeComponentVisitor, ExchangeComponentParams> SetExchangeComponent(this IVisitorBuilder<ExchangeComponentVisitor, ExchangeComponentParams> builder, ExchangeComponentParams exchangeComponentParams)
        {
            builder.SetParams(exchangeComponentParams);
            return builder;
        }
    }
}
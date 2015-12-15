var KnockoutMixin = {

    updateKnockout: function () {
        this.__koTrigger(!this.__koTrigger());
    },

    componentDidMount: function () {
        this.__koTrigger = ko.observable(true);
        this.__koModel = ko.computed(function () {
            this.__koTrigger(); // subscribe to changes of this...

            return {
                props: this.props,
                state: this.state
            };
        }, this);

        ko.applyBindings(this.__koModel, this.getDOMNode());
    },

    componentWillUnmount: function () {
        ko.cleanNode(this.getDOMNode());
    },

    componentDidUpdate: function () {
        this.updateKnockout();
    }
};

var reactHandler = ko.bindingHandlers.react = {
    render: function (el, Component, props) {
        React.render(
            React.createElement(Component, props),
            el
        );
    },

    init: function (el, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = valueAccessor();
        var Component = ko.unwrap(options.component || options.$);
        var props = ko.toJS(options.props || viewModel);

        reactHandler.render(el, Component, props);

        return { controlsDescendantBindings: true };
    },

    update: function (el, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = valueAccessor();
        var Component = ko.unwrap(options.component || options.$);
        var props = ko.toJS(options.props || viewModel);

        reactHandler.render(el, Component, props);

        return { controlsDescendantBindings: true };
    }
};
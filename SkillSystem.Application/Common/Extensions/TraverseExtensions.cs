namespace SkillSystem.Application.Common.Extensions;

public static class TraverseExtensions
{
    public static IEnumerable<TNode> Traverse<TNode>(
        this TNode rootNode,
        Func<TNode, IEnumerable<TNode>> childrenSelector)
    {
        return rootNode.Traverse(childrenSelector, _ => true, node => new[] { node });
    }

    public static IEnumerable<TValue> Traverse<TNode, TValue>(
        this TNode rootNode,
        Func<TNode, IEnumerable<TNode>> childrenSelector,
        Predicate<TNode> predicate,
        Func<TNode, IEnumerable<TValue>> valuesSelector)
    {
        if (rootNode == null)
            throw new ArgumentNullException(nameof(rootNode));
        if (childrenSelector == null)
            throw new ArgumentNullException(nameof(childrenSelector));
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
        if (valuesSelector == null)
            throw new ArgumentNullException(nameof(valuesSelector));

        return TraverseIterator(rootNode, childrenSelector, predicate, valuesSelector);
    }

    private static IEnumerable<TValue> TraverseIterator<TNode, TValue>(
        TNode rootNode,
        Func<TNode, IEnumerable<TNode>> childrenSelector,
        Predicate<TNode> predicate,
        Func<TNode, IEnumerable<TValue>> valuesSelector)
    {
        if (predicate(rootNode))
            foreach (var nodeValue in valuesSelector(rootNode))
                yield return nodeValue;
        var nodesChildren = new Stack<Stack<TNode>>();
        nodesChildren.Push(new Stack<TNode>(childrenSelector(rootNode)));
        while (nodesChildren.Count > 0)
        {
            var childNodes = nodesChildren.Peek();
            if (childNodes.Count > 0)
            {
                var currentNode = childNodes.Pop();
                if (predicate(currentNode))
                    foreach (var nodeValue in valuesSelector(currentNode))
                        yield return nodeValue;
                nodesChildren.Push(new Stack<TNode>(childrenSelector(currentNode)));
            }
            else
            {
                nodesChildren.Pop();
            }
        }
    }
}

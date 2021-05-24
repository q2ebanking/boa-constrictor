namespace Boa.Constrictor.Screenplay
{
    public class IsACollectionOfType<T>
    {
        #region Methods 

        public static HasSize<T> AndHasSizeThat(ICondition<int> condition)
        {
            return new HasSize<T>(condition);

/* Unmerged change from project 'Boa.Constrictor (netstandard2.0)'
Before:
        }
        
        public static EveryItem<T> AndEveryItem(ICondition<T> condition)
After:
        }

        public static EveryItem<T> AndEveryItem(ICondition<T> condition)
*/
        }

        public static EveryItem<T> AndEveryItem(ICondition<T> condition)
        {
            return new EveryItem<T>(condition);

/* Unmerged change from project 'Boa.Constrictor (netstandard2.0)'
Before:
        }
        
        public static AtLeastOneItem<T> AndAtLeastOneItem(ICondition<T> condition)
After:
        }

        public static AtLeastOneItem<T> AndAtLeastOneItem(ICondition<T> condition)
*/
        }

        public static AtLeastOneItem<T> AndAtLeastOneItem(ICondition<T> condition)
        {
            return new AtLeastOneItem<T>(condition);

/* Unmerged change from project 'Boa.Constrictor (netstandard2.0)'
Before:
        }
        
        public static FirstItem<T> AndTheFirstItem(ICondition<T> condition)
After:
        }

        public static FirstItem<T> AndTheFirstItem(ICondition<T> condition)
*/
        }

        public static FirstItem<T> AndTheFirstItem(ICondition<T> condition)
        {
            return new FirstItem<T>(condition);
        }

        public static LastItem<T> AndTheLastItem(ICondition<T> condition)
        {
            return new LastItem<T>(condition);
        }

        public static ItemAtPosition<T> AndTheItemAtPosition(int position, ICondition<T> condition)
        {
            return new ItemAtPosition<T>(position, condition);
        }

        #endregion
    }
}
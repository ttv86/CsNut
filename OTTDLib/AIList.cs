using System.Collections;
using System.Collections.Generic;

namespace OpenTTD
{
    public class AIList<T> : System.Collections.Generic.IEnumerable<(T, int)>
    {
        /// <summary>Type of sorter </summary>
        public enum SorterType
        {
            SORT_BY_VALUE,
            SORT_BY_ITEM,
        }

        /// <summary>Sort ascending </summary>
        public const bool SORT_ASCENDING = true;
        /// <summary>Sort descending </summary>
        public const bool SORT_DESCENDING = false;

        public AIList() { throw null; }

        /// <summary>Add a single item to the list.</summary><param name="item">the item to add. Should be unique, otherwise it is ignored.</param><param name="value">the value to assign.</param>
        public void AddItem(T item, int value = 0) { throw null; }

        /// <summary>Remove a single item from the list.</summary><param name="item">the item to remove. If not existing, it is ignored.</param>
        public void RemoveItem(T item) { throw null; }

        /// <summary>Clear the list, making Count() returning 0 and IsEmpty() returning true.</summary>
        public void Clear() { throw null; }

        /**
         * Check if an item is in the list.
         * @param item the item to check for.
         * @returns true if the item is in the list.
         */
        public bool HasItem(T item) { throw null; }

        /**
         * Go to the beginning of the list and return the item. To get the value use list.GetValue(list.Begin()).
         * @returns the first item.
         * @note returns 0 if beyond end-of-list. Use IsEnd() to check for end-of-list.
         */
        public T Begin() { throw null; }

        /**
         * Go to the next item in the list and return the item. To get the value use list.GetValue(list.Next()).
         * @returns the next item.
         * @note returns 0 if beyond end-of-list. Use IsEnd() to check for end-of-list.
         */
        public T Next() { throw null; }

        /// <summary>Check if a list is empty.</summary><returns>true if the list is empty.</returns>
        public bool IsEmpty() { throw null; }

        /**
         * Check if there is a element left. In other words, if this is false,
         * the last call to Begin() or Next() returned a valid item.
         * @returns true if the current item is beyond end-of-list.
         */
        public bool IsEnd() { throw null; }

        /// <summary>Returns the amount of items in the list.</summary><returns>amount of items in the list.</returns>
        public int Count() { throw null; }

        /**
         * Get the value that belongs to this item.
         * @param item the item to get the value from
         * @returns the value that belongs to this item.
         */
        public int GetValue(T item) { throw null; }

        /**
         * Set a value of an item directly.
         * @param item the item to set the value for.
         * @param value the value to give to the item
         * @returns true if we could set the item to value, false otherwise.
         * @note Changing values of items while looping through a list might cause
         *  entries to be skipped. Be very careful with such operations.
         */
        public bool SetValue(int item, int value) { throw null; }

        /**
         * Sort this list by the given sorter and direction.
         * @param sorter    the type of sorter to use
         * @param ascending if true, lowest value is on top, else at bottom.
         * @note the current item stays at the same place.
         * @see SORT_ASCENDING SORT_DESCENDING
         */
        public void Sort(AIList<T>.SorterType sorter, bool ascending) { throw null; }

        /**
         * Add one list to another one.
         * @param list The list that will be added to the caller.
         * @note All added items keep their value as it was in 'list'.
         * @note If the item already exists inside the caller, the value of the
         *  list that is added is set on the item.
         */
        public void AddList(AIList<T> list) { throw null; }

        /// <summary>Swap the contents of two lists.</summary><param name="list">The list that will be swapped with.</param>
        public void SwapList(AIList<T> list) { throw null; }

        /// <summary>Removes all items with a higher value than 'value'.</summary><param name="value">the value above which all items are removed.</param>
        public void RemoveAboveValue(int value) { throw null; }

        /// <summary>Removes all items with a lower value than 'value'.</summary><param name="value">the value below which all items are removed.</param>
        public void RemoveBelowValue(int value) { throw null; }

        /// <summary>Removes all items with a value above start and below end.</summary><param name="start">the lower bound of the to be removed values (exclusive).</param><param name="end">  the upper bound of the to be removed values (exclusive).</param>
        public void RemoveBetweenValue(int start, int end) { throw null; }

        /// <summary>Remove all items with this value.</summary><param name="value">the value to remove.</param>
        public void RemoveValue(int value) { throw null; }

        /// <summary>Remove the first count items.</summary><param name="count">the amount of items to remove.</param>
        public void RemoveTop(int count) { throw null; }

        /// <summary>Remove the last count items.</summary><param name="count">the amount of items to remove.</param>
        public void RemoveBottom(int count) { throw null; }

        /// <summary>Remove everything that is in the given list from this list (same item index that is).</summary><param name="list">the list of items to remove.</param>
        public void RemoveList(AIList<T> list) { throw null; }

        /// <summary>Keep all items with a higher value than 'value'.</summary><param name="value">the value above which all items are kept.</param>
        public void KeepAboveValue(int value) { throw null; }

        /// <summary>Keep all items with a lower value than 'value'.</summary><param name="value">the value below which all items are kept.</param>
        public void KeepBelowValue(int value) { throw null; }

        /// <summary>Keep all items with a value above start and below end.</summary><param name="start">the lower bound of the to be kept values (exclusive).</param><param name="end">  the upper bound of the to be kept values (exclusive).</param>
        public void KeepBetweenValue(int start, int end) { throw null; }

        /// <summary>Keep all items with this value.</summary><param name="value">the value to keep.</param>
        public void KeepValue(int value) { throw null; }

        /// <summary>Keep the first count items, i.e. remove everything except the first count items.</summary><param name="count">the amount of items to keep.</param>
        public void KeepTop(int count) { throw null; }

        /// <summary>Keep the last count items, i.e. remove everything except the last count items.</summary><param name="count">the amount of items to keep.</param>
        public void KeepBottom(int count) { throw null; }

        /// <summary>Keeps everything that is in the given list from this list (same item index that is).</summary><param name="list">the list of items to keep.</param>
        public void KeepList(AIList<T> list) { throw null; }

        /**
         * Give all items a value defined by the valuator you give.
         * @param valuator_function The function which will be doing the valuation.
         * @param params The params to give to the valuators (minus the first param,
         *  which is always the index-value we are valuating).
         * @note You may not add, remove or change (setting the value of) items while
         *  valuating. You may also not (re)sort while valuating.
         * @note You can write your own valuators and use them. Just remember that
         *  the first parameter should be the index-value, and it should return
         *  an integer.
         * @note Example:
         *  list.Valuate(ScriptBridge.GetPrice, 5)  { throw null; }
         *  list.Valuate(ScriptBridge.GetMaxLength)  { throw null; }
         *  function MyVal(bridge_id, myparam)
         *  {
         *    return myparam * bridge_id; // This is silly
         *  }
         *  list.Valuate(MyVal, 12)  { throw null; }
         */
        public void Valuate<U>(System.Func<T, U> vm) { throw null; }
        public void Valuate<U, P2>(System.Func<T, P2, U> vm, P2 param2) { throw null; }
        public void Valuate<U, P2, P3>(System.Func<T, P2, P3, U> vm, P2 param2, P3 param3) { throw null; }
        public void Valuate<U, P2, P3, P4>(System.Func<T, P2, P3, P4, U> vm, P2 param2, P3 param3, P4 param4) { throw null; }


        public IEnumerator<(T, int)> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
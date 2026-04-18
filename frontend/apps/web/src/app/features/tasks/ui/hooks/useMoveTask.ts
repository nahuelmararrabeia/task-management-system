import { useDI } from "@/app/providers";
import { KanbanColumnData } from "@task-management/ui";

export function useMoveTask(
  columns: KanbanColumnData[],
  setColumns: React.Dispatch<React.SetStateAction<KanbanColumnData[]>>
) {
  const { changeTaskStatus } = useDI().tasks;

  async function moveTask({
    cardId,
    fromColumnId,
    toColumnId,
    toIndex,
  }: {
    cardId: string;
    fromColumnId: string;
    toColumnId: string;
    toIndex: number;
  }) {
    const prevColumns = structuredClone(columns);

    setColumns((prev) => {
      const newColumns: KanbanColumnData[] = structuredClone(prev);

      const fromCol = newColumns.find(c => c.id === fromColumnId);
      const toCol = newColumns.find(c => c.id === toColumnId);

      if (!fromCol || !toCol) return prev;

      const card = fromCol.cards.find(c => c.id === cardId);
      if (!card) return prev;

      fromCol.cards = fromCol.cards.filter(c => c.id !== cardId);
      toCol.cards.splice(toIndex, 0, card);

      return newColumns;
    });


    try {
      await changeTaskStatus.execute(
        cardId,
        toColumnId
      );

    } catch (error) {
      setColumns(prevColumns);
      console.error(error);
    }
  }

  return {
    moveTask
  };
}
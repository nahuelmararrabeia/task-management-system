import { KanbanColumn } from "./KanbanColumn";
import { KanbanColumnData } from "./types";
import {
  DndContext,
  closestCenter,
  DragEndEvent,
} from "@dnd-kit/core";

interface KanbanBoardProps {
  columns: KanbanColumnData[];
  onCardMove?: (params: {
    cardId: string;
    fromColumnId: string;
    toColumnId: string;
    toIndex: number;
  }) => void;
}

export function KanbanBoard({ columns, onCardMove }: KanbanBoardProps) {
    function findColumnByCardId(cardId: string) {
    return columns.find((col) =>
      col.cards.some((c) => c.id === cardId)
    );
  }

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;

    if (!over) return;

    const cardId = String(active.id);
    const overId = String(over.id);

    const fromColumn = findColumnByCardId(cardId);
    const toColumn = columns.find((col) => col.id === overId);

    if (!fromColumn || !toColumn) return;

    const toIndex = toColumn.cards.length;

    onCardMove?.({
      cardId,
      fromColumnId: fromColumn.id,
      toColumnId: toColumn.id,
      toIndex,
    });
  };

  return (
    <DndContext
      collisionDetection={closestCenter}
      onDragEnd={handleDragEnd}
    >
      <div className="flex gap-4 overflow-x-auto p-4">
        {columns.map((column) => (
          <KanbanColumn key={column.id} column={column} />
        ))}
      </div>
    </DndContext>
  );
}
import { KanbanCard } from "./KanbanCard";
import { KanbanColumnData } from "./types";
import { useDroppable } from "@dnd-kit/core";

interface KanbanColumnProps {
  column: KanbanColumnData;
  onCardClick?: (id: string) => void;
}

export function KanbanColumn({ column, onCardClick }: KanbanColumnProps) {
  const { setNodeRef } = useDroppable({
    id: column.id,
  });

  return (
    <div
      ref={setNodeRef}
      className="min-w-70 w-full bg-gray-100 rounded-xl p-3 flex flex-col gap-3"
    >
      <div className="font-semibold text-sm px-1">
        {column.title}
      </div>

      <div className="flex flex-col gap-2">
        {column.cards.map((card) => (
          <KanbanCard
            key={card.id}
            id={card.id}
            onCardClick={onCardClick}
          >
            {card.content}
          </KanbanCard>
        ))}
      </div>
    </div>
  );
}
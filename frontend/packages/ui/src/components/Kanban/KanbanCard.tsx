import { ReactNode } from "react";
import { useDraggable } from "@dnd-kit/core";
import { CSS } from "@dnd-kit/utilities";

interface KanbanCardProps {
  id: string;
  children: ReactNode;
  className?: string;
  onCardClick?: (id: string) => void;
}

export function KanbanCard({ id, children, onCardClick }: KanbanCardProps) {
  const { attributes, listeners, setNodeRef, transform } =
    useDraggable({ id });

  const style = {
    transform: CSS.Translate.toString(transform),
  };

  const handleClick = () => {
    onCardClick?.(id);
  }

  return (
    <div
      ref={setNodeRef}
      style={style}
      className="bg-white flex justify-between rounded-lg shadow-sm p-3 text-sm"
    >
      <div onClick={handleClick} className="cursor-pointer">
        {children}
      </div>

      <div {...listeners} {...attributes} className="cursor-grab">
        ⠿
      </div>
    </div>
  );
}
import { ReactNode } from "react";
import { useDraggable } from "@dnd-kit/core";
import { CSS } from "@dnd-kit/utilities";

interface KanbanCardProps {
  id: string;
  children: ReactNode;
  className?: string;
}

export function KanbanCard({ id, children }: KanbanCardProps) {
  const { attributes, listeners, setNodeRef, transform } =
    useDraggable({ id });

  const style = {
    transform: CSS.Translate.toString(transform),
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      {...listeners}
      {...attributes}
      className="bg-white rounded-lg shadow-sm p-3 text-sm cursor-grab"
    >
      {children}
    </div>
  );
}
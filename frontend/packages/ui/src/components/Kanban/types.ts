export type KanbanColumnId = string;
export type KanbanCardId = string;

export interface KanbanCardData {
  id: KanbanCardId;
  content: React.ReactNode;
}

export interface KanbanColumnData {
  id: KanbanColumnId;
  title: React.ReactNode;
  cards: KanbanCardData[];
}
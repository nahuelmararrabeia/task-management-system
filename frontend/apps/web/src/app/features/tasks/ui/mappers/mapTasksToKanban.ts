import { Task } from "../../entities/Task";
import { KanbanColumnData } from "@task-management/ui";

export function mapTasksToKanban(tasks: Task[]): KanbanColumnData[] {
  const columns: KanbanColumnData[] = [
    { id: "Pending", title: "To Do", cards: [] },
    { id: "InProgress", title: "In Progress", cards: [] },
    { id: "Completed", title: "Done", cards: [] },
  ];

  tasks.forEach((task) => {
    const column = columns.find(c => c.id === task.status);

    if (!column) return;

    column.cards.push({
      id: task.id!,
      content: task.title,
    });
  });

  return columns;
}
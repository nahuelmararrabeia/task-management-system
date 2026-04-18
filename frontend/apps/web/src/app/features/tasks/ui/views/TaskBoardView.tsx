"use client";

import { KanbanBoard, KanbanColumnData } from "@task-management/ui";
import { useTasks } from "../hooks/useTasks";
import { mapTasksToKanban } from "../mappers/mapTasksToKanban";
import { useEffect, useState } from "react";
import { useMoveTask } from "../hooks/useMoveTask";

export function TaskBoardView() {
  const { tasks, loading } = useTasks();
  const [columns, setColumns] = useState<KanbanColumnData[]>([]);

  useEffect(() => {
    setColumns(mapTasksToKanban(tasks));
  }, [tasks]);

  const { moveTask } = useMoveTask(columns, setColumns);
  
  if (loading) return <div>Loading...</div>;

  return (
    <KanbanBoard
     columns={columns}
     onCardMove={moveTask} />
);
}
"use client";

import {  KanbanBoard, KanbanColumnData, Title } from "@task-management/ui";
import { useTasks } from "../hooks/useTasks";
import { mapTasksToKanban } from "../mappers/mapTasksToKanban";
import { useEffect, useState } from "react";
import { useMoveTask } from "../hooks/useMoveTask";
import { useAddNewTask } from "../hooks/useAddNewTask";
import { CreateTaskButton } from "../components/CreateTaskButton";
import { useTask } from "../hooks/useTask";
import { TaskDrawer } from "../components/TaskDrawer";

export function TaskBoardView() {
  const { tasks, loadingGetTasks } = useTasks();
  const {
    selectedTask,
    loadTask,
    clearTask,
  } = useTask();
  const [columns, setColumns] = useState<KanbanColumnData[]>([]);
  
  useEffect(() => {
    setColumns(mapTasksToKanban(tasks));
  }, [tasks]);
  
  const { tryCreateTask, loadingCreateTask } = useAddNewTask();
  const { moveTask } = useMoveTask(columns, setColumns);

  async function handleCreateTask(title: string) {
    await tryCreateTask(title, setColumns);   
  }
  
  const handleCardClick = (id: string) => {
    loadTask(id);
  };

  if (loadingGetTasks) return <div>Loading...</div>;

  return (
    <div>
      <Title as="h1" size="xl" className="ml-4">
        Task Board
      </Title>
      <CreateTaskButton onConfirmCreate={handleCreateTask} />
      <KanbanBoard
        columns={columns}
        onCardMove={moveTask}
        onCardClick={handleCardClick}
      />
      <TaskDrawer
        task={selectedTask}
        onClose={clearTask}
      />
    </div>
);
}
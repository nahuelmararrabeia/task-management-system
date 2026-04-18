import type { Meta, StoryObj } from "@storybook/react";
import { KanbanColumn } from "./KanbanColumn";

const meta: Meta<typeof KanbanColumn> = {
  title: "Components/Kanban/KanbanColumn",
  component: KanbanColumn,
};

export default meta;

type Story = StoryObj<typeof KanbanColumn>;

export const Default: Story = {
  args: {
    column: {
      id: "todo",
      title: "To Do",
      cards: [
        { id: "1", content: "Task 1" },
        { id: "2", content: "Task 2" },
      ],
    },
  },
};

export const Empty: Story = {
  args: {
    column: {
      id: "empty",
      title: "Empty Column",
      cards: [],
    },
  },
};
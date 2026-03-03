export const TrainingFormat = {
  FullTime: 1,
  PartTime: 2,
} as const;

export type TrainingFormat =
  (typeof TrainingFormat)[keyof typeof TrainingFormat];

export const TrainingFormatUtils = {
  getDisplayName(trainingFormat: TrainingFormat): string {
    switch (trainingFormat) {
      case TrainingFormat.FullTime:
        return "Очный";
      case TrainingFormat.PartTime:
        return "Заочный";
      default:
        return "";
    }
  },
} as const;

//
//  PropDescriptionViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 23.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "BaseViewController.h"

@interface PropDescriptionViewController : BaseViewController
@property (weak, nonatomic) IBOutlet UITextView *propFullText;
@property (weak, nonatomic) NSString *propText;
@end
